import executeQuery from '../query';
import {
  DBConfig
} from "../config";

var mysql = require('mysql');
var table = DBConfig.dbs.slide;

export default {
  byId: async(id) => {
    var sql = "DELETE FROM " + table + " WHERE id=?";
    var inserts = [id];
    sql = mysql.format(sql, inserts);
    return executeQuery(sql);
  }
};
