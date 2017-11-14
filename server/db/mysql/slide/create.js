import executeQuery from '../query';
import mysql from 'mysql';
import DBConfig from "../../config";

let table = DBConfig.mysql.dbs.slide;

export default {
  one: async(item) => {
    let sql = "INSERT INTO " + table + " (id, presentation_id, sequence) VALUES(DEFAULT,?,?)";
    let inserts = [item.presentation_id, item.sequence]
    sql = mysql.format(sql, inserts);
    return executeQuery(sql);
  }
};
