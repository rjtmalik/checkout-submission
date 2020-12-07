FROM alpine

WORKDIR app

ADD MasterCard .

RUN apk add --update nodejs npm 

RUN npm install -g stubby

ENTRYPOINT [ "stubby", "-d", "stubby.yml", "-s", "2020" ]