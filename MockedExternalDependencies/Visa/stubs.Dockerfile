FROM alpine

WORKDIR app

ADD Visa .

RUN apk add --update nodejs npm 

RUN npm install -g stubby

ENTRYPOINT [ "stubby", "-d", "stubby.yml", "-s", "1010" ]