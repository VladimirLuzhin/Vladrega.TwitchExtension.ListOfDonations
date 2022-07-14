# Running Backend

### IDE
You can run backend in your favorite IDE like Visual Studio or Rider. Just press F5.

### Docker

```
docker build -t donationsbackend -f .\Vladrega.ListOfDonations\Dockerfile .
docker run -d -v /path/to/ssl_certs:/app/certs --env-file env.list -p 443:30500 donationsbackend

P.S env.list file must contain next environment variables:
- DatabaseConfig__Login
- DatabaseConfig__Host
- DatabaseConfig__Password
- DatabaseConfig__Database
- TwitchConfig__ExtensionId
```
