How to deploy to a Debian 11 server
===================================

Publishing
===========

::
    $ dotnet publish -c Release

TODO: Why does it make a net6.0 directory with all files, and also a publish directory with all files? Only the net6.0 directory works.


Running on a web server
========================

::
    $ wget https://packages.microsoft.com/config/debian/11/packages-microsoft-prod.deb -O packages-microsoft-prod.deb
    $ sudo dpkg -i packages-microsoft-prod.deb
    $ sudo apt update
    $ sudo apt install aptnetcore-runtime-6.0
    ... copy the files to /var/www/northwind/ and set permissions appropriately
    $ dotnet run Microsoft.Restier.Samples.Northwind.AspNetCore.dll # to see if it works.

Make a service. Copy to /etc/systemd/system/Northwind.service

::
    [Unit]
    Description=Northwind OData service
    [Service]
    WorkingDirectory=/var/www/northwind
    ExecStart=/usr/bin/dotnet /var/www/northwind/Microsoft.Restier.Samples.Northwind.AspNetCore.dll
    Restart=always
    RestartSec=10
    SyslogIdentifier=northwind
    User=www-data
    Environment=ASPNETCORE_ENVIRONMENT=Production
    [Install]
    WantedBy=multi-user.target

::
    $ sudo systemctl enable northwind 
    $ sudo systemctl start northwind
    $ sudo systemctl status nortwind

