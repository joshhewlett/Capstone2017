import executeQuery from '../query';
import {
  DBConfig
} from "../config";

var mysql = require('mysql');
var table = DBConfig.dbs.slide;

export default {
  byId: async(id) => {
    var sql = "SELECT * FROM " + table + " WHERE id=?";
    sql = mysql.format(sql, [id]);
    return executeQuery(sql);
  },

  byPresentationId: async(id) => {
    var sql = "SELECT " + table + ".id, " + table + ".physician_id, " + table + ".child_id, " + table + ".name, " + table + ".reason, " + table + ".dosage, " + table + ".original_amount, " + table + ".units, " + table + ".date FROM (" + table + " JOIN " + DBConfig.dbs.child + " ON " + DBConfig.dbs.child + ".id=" + table + ".child_id) WHERE " + DBConfig.dbs.child + ".id=?";
    sql = mysql.format(sql, [id]);
    return executeQuery(sql);
  },

  all: async() => {
    var sql = "SELECT * FROM " + table;
    return executeQuery(sql);
  }
};
