// Presentation SELECT
import executeQuery from '../query';
import mysql from 'mysql';
import DBConfig from "../../config";

let table = DBConfig.mysql.dbs.presentation;

export default {
  // Select a Presentation object by ID
  byId: async(id) => {
    let sql = "SELECT * FROM " + table + " WHERE id=?";
    sql = mysql.format(sql, [id]);
    return executeQuery(sql);
  },
  // Select Presentation objects by USER_ID
  byUser: async(userID) => {
    let sql = "SELECT * FROM " + table + " WHERE user_id=?";
    sql = mysql.format(sql, [userID]);
    return executeQuery(sql);
  },
  // Select Presentation objects by USER_NAME
  byName: async(name) => {
    let sql = "SELECT * FROM " + table + " WHERE name LIKE ?";
    sql = mysql.format(sql, [name]);
    return executeQuery(sql);
  },
  // Select Presentation objects by DESCRIPTION
  byDescription: async(description) => {
    let sql = "SELECT * FROM " + table + " WHERE description LIKE ?";
    sql = mysql.format(sql, [description]);
    return executeQuery(sql);
  }
};
