FROM node:carbon

WORKDIR /usr/src/app

COPY server/package.json .
COPY server/package-lock.json .

RUN npm install

COPY server .

EXPOSE 3000

CMD [ "npm", "start" ]