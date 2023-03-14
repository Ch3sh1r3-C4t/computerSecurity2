## ODF Attack Script
Execute certificate duplication attacks on ODF documents
usage: `python3 odf_attack.py [-h] --attack_type {macro,content} --trusted_file TRUSTED_FILE --edited_file EDITED_FILE --output_file OUTPUT_FILE`

This script takes the following as input:
(It is important to note that the machine MUST trust the certificate of the trusted entity)
- The type of attack being executed: macro | content
- An odt file that was signed by a trusted entity
- An odt file that was based of the trusted file. However, the content or macro of this file was changed and the file was resigned by the attacker using their self-signed certificate
- An output file name that the results of the attack would be written to. Should be `.odt`


## Prerequisites 
- Python 3.10.9
- lxml (https://lxml.de/installation.html)
- shutil (https://docs.python.org/3/library/shutil.html)


