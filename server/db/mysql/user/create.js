import executeQuery from '../query';
import {
    DBConfig
} from "../config";

var mysql = require('mysql');
var table = DBConfig.dbs.user;

export default {
    one: async(item) => {
        var sql = "INSERT INTO " + table + " (id,email) VALUES(DEFAULT,?)";
        var inserts = [item.email]
        sql = mysql.format(sql, inserts);
        return executeQuery(sql);
    }
};