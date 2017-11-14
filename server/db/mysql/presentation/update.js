import executeQuery from '../query';
import mysql from 'mysql';
import DBConfig from "../../config";

let table = DBConfig.mysql.dbs.presentation;

export default {
  byId: async(id, presentation) => {
    if (!id || !presentation.name || !presentation.description || !presentation.presented || !presentation.user_id) {
      throw "Missing attribute on presentation";
    }
    let sql = "UPDATE " + table + " SET name = ?, description = ?, presented = ?, user_id = ? WHERE id = ?"
    let inserts = [presentation.name, presentation.description, presentation.presented, presentation.user_id, id];
    sql = mysql.format(sql, inserts);
    return executeQuery(sql);
  }
};
