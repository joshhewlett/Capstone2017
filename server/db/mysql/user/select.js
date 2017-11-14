// User SELECT
import executeQuery from '../query';
import mysql from 'mysql';
import DBConfig from "../../config";

let table = DBConfig.mysql.dbs.user;

export default {
  // Select a User object by ID
  byId: async(id) => {
    let sql = "SELECT * FROM " + table + " WHERE id=?";
    sql = mysql.format(sql, [id]);
    return executeQuery(sql);
  },
  // Select a User object by EMAIL
  byEmail: async(email) => {
    let sql = "SELECT * FROM " + table + " WHERE email LIKE ?";
    sql = mysql.format(sql, [email]);
    return executeQuery(sql);
  }
};
