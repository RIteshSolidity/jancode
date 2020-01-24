$cert = New-SelfSignedCertificate -DnsName adadasd.com -CertStoreLocation Cert:\LocalMachine\My

$mycert = Cert:\LocalMachine\my\ + $cert.Thumbprint

$filename = "adadad.pfx"

Export-PfxCertificate -Cert $mycert -FilePath
