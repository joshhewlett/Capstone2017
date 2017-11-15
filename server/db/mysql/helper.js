// Singleton for MySQL Database connection
import mysql from 'promise-mysql';
import DBConfig from "../../config";

let connection = null;

async function Connect() {
  let connection = await mysql.createConnection({
    host: DBConfig.mysql.host,
    user: DBConfig.mysql.username,
    password: DBConfig.mysql.password,
    database: "capstone_db"
  });
  return connection;
}

export default async() => {
  if (!connection) {
    connection = await new Connect();
  }
  return connection;
};
