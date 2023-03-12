# computerSecurity2

## Usefull Links:
- Paper page: https://www.usenix.org/conference/usenixsecurity22/presentation/rohlmann
- Paper presentation video:  https://youtu.be/DLW4iJ5l2OM
- Repo with POC: https://github.com/RUB-NDS/DocumentSignatureValidator
- Link to VM: https://liveuclac-my.sharepoint.com/:u:/g/personal/ucabaij_ucl_ac_uk/ETLX7CAif9JAkU6Bs3K0seABGhU_f29jculCZYqXplQw_w?e=LBaqKR

-----------

## Attack-GUI steps

### Attack 1 - Content Manipulation with Certificate Doubling

**Step 1:**  Steven has a document which is already signed with a certificate that is already trusted by the machine.

**Step 2:** Steven opens the file in LibreOffice. He sees the message in LibreOffice that says "this document has a valid signature". We tell him to close the file.

**Step 3:**  Steven clicks 'next' on the GUI and the same file is modified with new content. We tell him to open the 'now-modified' file  in LibreOffice. He sees in LibreOffice that "this file contains an invalid signature". He closes the file.

**Step 4:**  Steven clicks 'next' on the GUI and the file is signed with the privkey of the attacker (brendon's python script is executed). He opens the 'now-modified' file again. He sees in LibreOffice that "this file contains a valid signature". 

**The result:** Steven sees that the 'now-modified' file contains a valid signature.



### Attack 2 - Macro Manipulation with Certificate Doubling

**Step 1:**   Steven has a document which is already signed with a certificate that is already trusted by the machine. This file contains a normal, simple macro. He sees the normal macro being executed.

**Step 2:**  Steven opens the file in LibreOffice. He sees the message in LibreOffice that says "this document has a valid signature". We tell him to close the file.

**Step 3:**  Steven clicks 'next' on the GUI and the malicious macro is now injected in the xml file of the .odt file he had opened in LibreOffice (TODO script). We tell him to open the file. He sees in LibreOffice that "this file contains an invalid signature". He closes the file.

**Step 4:**  Steven clicks 'next' on the GUI and the document is signed with the privkey of the attacker (eleni's python script is executed). He opens the file again. He sees in LibreOffice that "this file contains a valid signature". He also sees the malicious macro being executed. 

**The result:** He can see that a file with malicious macros contains a valid signature and that these macros get executed.



**TODO**
i. Combine Eleni's and Brendon script essentially in one since it seems that the logic is the same.
ii. Create script that injects the malicious macro in the XML of the .odt file.
