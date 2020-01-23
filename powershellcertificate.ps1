$cert = New-SelfSignedCertificate -DnsName azureblueprints.org -CertStoreLocation Cert:\LocalMachine\My

$certReference = Get-Item $("Cert:\LocalMachine\My\" + $cert.Thumbprint)

$password = ConvertTo-SecureString -String riteshmomdi -AsPlainText -Force

Export-PfxCertificate -Cert $certReference -FilePath "C:\azureblueprints.pfx" -Password $password

