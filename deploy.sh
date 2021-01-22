dotnet publish -c Release
docker build -t keeprapp ./bin/Release/net5.0/publish
docker tag keeprapp registry.heroku.com/keepkeepr/web
docker push registry.heroku.com/keepkeepr/web
heroku container:release web -a keepkeepr
echo press any key
read end