FROM microsoft/aspnetcore:1.1
ARG source
WORKDIR /app
EXPOSE 80
## install nodejs for angular, webpack middleware
RUN apt-get update  
RUN apt-get -f install  
RUN apt-get install -y wget  
RUN wget -qO- https://deb.nodesource.com/setup_6.x | bash -  
RUN apt-get install -y build-essential nodejs
RUN npm install
RUN npm -v
RUN node -v
RUN npm install -g @angular/cli
COPY ${source:-obj/Docker/publish} .
ENTRYPOINT ["dotnet", "Facebook.WebApplication.dll"]
