FROM node:19-alpine

WORKDIR /usr/src/app

RUN npm install -g pnpm

COPY . .
RUN pnpm install

RUN npx nx build api-nest

EXPOSE 44310
WORKDIR /usr/src/app/dist/apps/api-nest

CMD [ "node", "main.js" ]
