import executeQuery from '../query';
import {
    DBConfig
} from "../../config";

var mysql = require('mysql');
var table = DBConfig.mysql.dbs.presentation;

export default {
    byId: async(id) => {
        var sql = "SELECT * FROM " + table + " WHERE id=?";
        sql = mysql.format(sql, [id]);
        return executeQuery(sql);
    },
    byUser: async(userID) => {
        var sql = "SELECT * FROM " + table + " WHERE user_id=?";
        sql = mysql.format(sql, [userID]);
        return executeQuery(sql);
    },
    byName: async(name) => {
        var sql = "SELECT * FROM " + table + " WHERE name LIKE ?";
        sql = mysql.format(sql, [name]);
        return executeQuery(sql);
    },
    byDescription: async(description) => {
        var sql = "SELECT * FROM " + table + " WHERE description LIKE ?";
        sql = mysql.format(sql, [description]);
        return executeQuery(sql);
    }
};
