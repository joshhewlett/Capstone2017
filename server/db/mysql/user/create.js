import executeQuery from '../query';
import mysql from 'mysql';
import DBConfig from "../../config";

let table = DBConfig.mysql.dbs.user;

export default {
  one: async(item) => {
    let sql = "INSERT INTO " + table + " (id,email) VALUES(DEFAULT,?)";
    let inserts = [item.email]
    sql = mysql.format(sql, inserts);
    return executeQuery(sql);
  }
};
