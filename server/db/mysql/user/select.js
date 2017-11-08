import executeQuery from '../query';
import {
    DBConfig
} from "../config";

var mysql = require('mysql');
var table = DBConfig.dbs.user;

export default {
    byId: async(id) => {
        var sql = "SELECT * FROM " + table + " WHERE id=?";
        sql = mysql.format(sql, [id]);
        return executeQuery(sql);
    },
    byEmail: async(email) => {
        var sql = "SELECT * FROM " + table + " WHERE email LIKE ?";
        sql = mysql.format(sql, [email]);
        return executeQuery(sql);
    }
};
