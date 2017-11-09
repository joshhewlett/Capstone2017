import executeQuery from '../query';
import {
  DBConfig
} from "../config";

var mysql = require('mysql');
var table = DBConfig.dbs.user;

export default {
  byId: async(id, user) => {
    if (!id || !user.email) {
      throw "Missing attribute on user";
    }
    var sql = "UPDATE " + table + " SET email = ? WHERE id = ?"
    var inserts = [user.email, id];
    sql = mysql.format(sql, inserts);
    return executeQuery(sql);
  }
};
