import executeQuery from '../query';
import mysql from 'mysql';
import DBConfig from "../../config";

let table = DBConfig.mysql.dbs.slide;

export default {
  byId: async(id) => {
    let sql = "SELECT * FROM " + table + " WHERE id=?";
    sql = mysql.format(sql, [id]);
    return executeQuery(sql);
  },

  byPresentationId: async(id) => {
    let sql = "SELECT " + table + ".id, " + table + ".physician_id, " + table + ".child_id, " + table + ".name, " + table + ".reason, " + table + ".dosage, " + table + ".original_amount, " + table + ".units, " + table + ".date FROM (" + table + " JOIN " + DBConfig.mysql.dbs.child + " ON " + DBConfig.mysql.dbs.child + ".id=" + table + ".child_id) WHERE " + DBConfig.mysql.dbs.child + ".id=?";
    sql = mysql.format(sql, [id]);
    return executeQuery(sql);
  },

  all: async() => {
    let sql = "SELECT * FROM " + table;
    return executeQuery(sql);
  }
};
