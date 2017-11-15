// Slide UPDATE
import executeQuery from '../query';
import mysql from 'mysql';
import DBConfig from "../../config";

let table = DBConfig.mysql.dbs.slide;

export default {
  // Update a Slide object by ID
  byId: async(id, slide) => {
    if (!id || !slide.sequence) {
      throw "Missing attribute on slide";
    }
    let sql = "UPDATE " + table + " SET email = ? WHERE id = ?"
    let inserts = [slide.sequence, id];
    sql = mysql.format(sql, inserts);
    return executeQuery(sql);
  }
};
