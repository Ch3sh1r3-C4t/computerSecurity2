<#
    script used to gather different information abou the systems without using any 'net', 'ipconfig', 'whoami', 'netstat', or other system commands to help avoid detection.
    HostRecon Function: Invoke-HostRecon
    Author: Beau Bullock (@dafthack) (https://github.com/dafthack/HostRecon/blob/master/HostRecon.ps1)
    License: BSD 3-Clause
    Required Dependencies: None
    Optional Dependencies: None
    
    TODO:
    [] Add abfuscation
    #>



Write-Output "[*] Hostname"
$Computer = $env:COMPUTERNAME
$Computer
Write-Output "`n"

#IP Information
Write-Output "[*] IP Address Info"
$ipinfo = Get-WmiObject -Class Win32_NetworkAdapterConfiguration -Filter 'IPEnabled = True'| Select-Object IPAddress,Description | Format-Table -Wrap | Out-String
$ipinfo
Write-Output "`n"

#Current user and domain
Write-Output "[*] Current Domain and Username"

$currentuser = $env:USERNAME
Write-Output "Domain = $env:USERDOMAIN"
Write-Output "Current User = $env:USERNAME"
Write-Output "`n"

#All local users
Write-Output "[*] Local Users of this system"
$locals = Get-WmiObject -Class Win32_UserAccount -Filter  "LocalAccount='True'" | Select-Object Name 
$locals
Write-Output "`n"

#Local Admins group
Write-Output "[*] Local Admins of this system"
$Admins = Get-WmiObject win32_groupuser | Where-Object { $_.GroupComponent -match 'administrators' -and ($_.GroupComponent -match "Domain=`"$env:COMPUTERNAME`"")} | ForEach-Object {[wmi]$_.PartComponent } | Select-Object Caption,SID | format-table -Wrap | Out-String
$Admins
Write-Output "`n"

#DNS Cache Information

Write-Output "[*] DNS Cache"

 try{
    $dnscache = Get-WmiObject -query "Select * from MSFT_DNSClientCache" -Namespace "root\standardcimv2" -ErrorAction stop | Select-Object Entry,Name,Data | Format-Table -Wrap | Out-String
    $dnscache
    }
    catch
        {
        Write-Output "There was an error retrieving the DNS cache."
        }
    Write-Output "`n"

    #Shares

    Write-Output "[*] Share listing"
    $shares = @()
    $shares = Get-WmiObject -Class Win32_Share | Format-Table -Wrap | Out-String
    $shares
    Write-Output "`n"

    #Scheduled Tasks

    Write-Output "[*] List of scheduled tasks"
    $schedule = new-object -com("Schedule.Service")
    $schedule.connect() 
    $tasks = $schedule.getfolder("\").gettasks(0) | Select-Object Name | Format-Table -Wrap | Out-String
    If ($tasks.count -eq 0)
        {
        Write-Output "[*] Task scheduler appears to be empty"
        }
    If ($tasks.count -ne 0)
        {
        $tasks
        }
    Write-Output "`n"

    #Proxy information

    Write-Output "[*] Proxy Info"
    $proxyenabled = (Get-ItemProperty -Path 'HKCU:\Software\Microsoft\Windows\CurrentVersion\Internet Settings').proxyEnable
    $proxyserver = (Get-ItemProperty -Path 'HKCU:\Software\Microsoft\Windows\CurrentVersion\Internet Settings').proxyServer

    If ($proxyenabled -eq 1)
        {
            Write-Output "A system proxy appears to be enabled."
            Write-Output "System proxy located at: $proxyserver"
        }
    Elseif($proxyenabled -eq 0)
        {
            Write-Output "There does not appear to be a system proxy enabled."
        }
    Write-Output "`n"

    #Getting AntiVirus Information


    Write-Output "[*] Checking if AV is installed"

    $AV = Get-WmiObject -Namespace "root\SecurityCenter2" -Query "SELECT * FROM AntiVirusProduct" 

    If ($AV -ne "")
        {
            Write-Output "The following AntiVirus product appears to be installed:" $AV.displayName
        }
    If ($AV -eq "")
        {
            Write-Output "No AV detected."
        }
    Write-Output "`n"

    #Getting Local Firewall Status

    Write-Output "[*] Checking local firewall status."
    $HKLM = 2147483650
    $reg = get-wmiobject -list -namespace root\default -computer $computer | where-object { $_.name -eq "StdRegProv" }
    $firewallEnabled = $reg.GetDwordValue($HKLM, "System\ControlSet001\Services\SharedAccess\Parameters\FirewallPolicy\StandardProfile","EnableFirewall")
    $fwenabled = [bool]($firewallEnabled.uValue)

    If($fwenabled -eq $true)
        {
            Write-Output "The local firewall appears to be enabled."
        }
    If($fwenabled -ne $true)
        {
            Write-Output "The local firewall appears to be disabled."
        }
    Write-Output "`n"

  

    #Process Information

    Write-Output "[*] Running Processes"

    $processes = Get-Process | Select-Object ProcessName,Id,Description,Path 
    $processout = $processes | Format-Table -Wrap | Out-String
    $processout
    Write-Output "`n"

    #Checking for common security products

    Write-Output "[*] Checking for Sysinternals Sysmon"
    try
        {
        $sysmondrv = Get-ChildItem "$env:SystemRoot\sysmondrv.sys" -ErrorAction Stop
        if ($sysmondrv)
            {
            Write-Output "The Sysmon driver $($sysmondrv.VersionInfo.FileVersion) (sysmondrv.sys) was found. System activity may be monitored."
            }
        }
    catch
        {
        Write-Output "The Sysmon driver was not found."
        }
    Write-Output "`n"

    Write-Output "[*] Checking for common security product processes"
    $processnames = $processes | Select-Object ProcessName
    Foreach ($ps in $processnames)
            {
            #AV
            if ($ps.ProcessName -like "*mcshield*")
                {
                Write-Output ("Possible McAfee AV process " + $ps.ProcessName + " is running.")
                }
            if (($ps.ProcessName -like "*windefend*") -or ($ps.ProcessName -like "*MSASCui*") -or ($ps.ProcessName -like "*msmpeng*") -or ($ps.ProcessName -like "*msmpsvc*"))
                {
                Write-Output ("Possible Windows Defender AV process " + $ps.ProcessName + " is running.")
                }
            if ($ps.ProcessName -like "*WRSA*")
                {
                Write-Output ("Possible WebRoot AV process " + $ps.ProcessName + " is running.")
                }
            if ($ps.ProcessName -like "*savservice*")
                {
                Write-Output ("Possible Sophos AV process " + $ps.ProcessName + " is running.")
                }
            if (($ps.ProcessName -like "*TMCCSF*") -or ($ps.ProcessName -like "*TmListen*") -or ($ps.ProcessName -like "*NTRtScan*"))
                {
                Write-Output ("Possible Trend Micro AV process " + $ps.ProcessName + " is running.")
                }
            if (($ps.ProcessName -like "*symantec antivirus*") -or ($ps.ProcessName -like "*SymCorpUI*") -or ($ps.ProcessName -like "*ccSvcHst*") -or ($ps.ProcessName -like "*SMC*")  -or ($ps.ProcessName -like "*Rtvscan*"))
                {
                Write-Output ("Possible Symantec AV process " + $ps.ProcessName + " is running.")
                }
            if ($ps.ProcessName -like "*mbae*")
                {
                Write-Output ("Possible MalwareBytes Anti-Exploit process " + $ps.ProcessName + " is running.")
                }
            #if ($ps.ProcessName -like "*mbam*")
               # {
               # Write-Output ("Possible MalwareBytes Anti-Malware process " + $ps.ProcessName + " is running.")
               # }
            #AppWhitelisting
            if ($ps.ProcessName -like "*Parity*")
                {
                Write-Output ("Possible Bit9 application whitelisting process " + $ps.ProcessName + " is running.")
                }
            #Behavioral Analysis
            if ($ps.ProcessName -like "*cb*")
                {
                Write-Output ("Possible Carbon Black behavioral analysis process " + $ps.ProcessName + " is running.")
                }
            if ($ps.ProcessName -like "*bds-vision*")
                {
                Write-Output ("Possible BDS Vision behavioral analysis process " + $ps.ProcessName + " is running.")
                } 
            if ($ps.ProcessName -like "*Triumfant*")
                {
                Write-Output ("Possible Triumfant behavioral analysis process " + $ps.ProcessName + " is running.")
                }
            if ($ps.ProcessName -like "CSFalcon")
                {
                Write-Output ("Possible CrowdStrike Falcon EDR process " + $ps.ProcessName + " is running.")
                }
            #Intrusion Detection
            if ($ps.ProcessName -like "*ossec*")
                {
                Write-Output ("Possible OSSEC intrusion detection process " + $ps.ProcessName + " is running.")
                } 
            #Firewall
            if ($ps.ProcessName -like "*TmPfw*")
                {
                Write-Output ("Possible Trend Micro firewall process " + $ps.ProcessName + " is running.")
                } 
            #DLP
            if (($ps.ProcessName -like "dgagent") -or ($ps.ProcessName -like "DgService") -or ($ps.ProcessName -like "DgScan"))
                {
                Write-Output ("Possible Verdasys Digital Guardian DLP process " + $ps.ProcessName + " is running.")
                }   
            if ($ps.ProcessName -like "kvoop")
                {
                Write-Output ("Possible Unknown DLP process " + $ps.ProcessName + " is running.")
                }                       
            }
    Write-Output "`n"