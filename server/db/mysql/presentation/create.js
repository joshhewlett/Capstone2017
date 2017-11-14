// Presentation CREATE
import executeQuery from '../query';
import mysql from 'mysql';
import DBConfig from "../../config";

let table = DBConfig.mysql.dbs.presentation;

export default {
  // Create a Presentation object
  one: async(item) => {
    let sql = "INSERT INTO " + table + " (id,name,description,presented,user_id) VALUES(DEFAULT,?,?,?,?)";
    let inserts = [item.name, item.description, item.presented, item.user_id];
    sql = mysql.format(sql, inserts);
    return executeQuery(sql);
  }
};
