import executeQuery from '../query';
import {
  DBConfig
} from "../config";

var mysql = require('mysql');
var table = DBConfig.dbs.slide;

export default {
  one: async(item) => {
    var sql = "INSERT INTO " + table + " (id, presentation_id, sequence) VALUES(DEFAULT,?,?)";
    var inserts = [item.presentation_id, item.sequence]
    sql = mysql.format(sql, inserts);
    return executeQuery(sql);
  }
};
