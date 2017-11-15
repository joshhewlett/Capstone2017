// User UPDATE
import executeQuery from '../query';
import mysql from 'mysql';
import DBConfig from "../../config";

let table = DBConfig.mysql.dbs.user;

export default {
  // Update a User object by ID
  byId: async(id, user) => {
    if (!id || !user.email) {
      throw "Missing attribute on user";
    }
    let sql = "UPDATE " + table + " SET email = ? WHERE id = ?"
    let inserts = [user.email, id];
    sql = mysql.format(sql, inserts);
    return executeQuery(sql);
  }
};
