// Presentation DELETE
import executeQuery from '../query';
import mysql from 'mysql';
import DBConfig from "../../config";

let table = DBConfig.mysql.dbs.presentation;

export default {
  // Delete a Presentation by ID
  byId: async(id) => {
    let sql = "DELETE FROM " + table + " WHERE id=?";
    let inserts = [id];
    sql = mysql.format(sql, inserts);
    return executeQuery(sql);
  }
};
