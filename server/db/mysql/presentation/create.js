import executeQuery from '../query';
import {
    DBConfig
} from "../../config";

var mysql = require('mysql');
var table = DBConfig.mysql.dbs.presentation;

export default {
    one: async(item) => {
        var sql = "INSERT INTO " + table + " (id,name,description,presented,user_id) VALUES(DEFAULT,?,?,?,?)";
        var inserts = [item.name, item.description, item.presented, item.user_id];
        sql = mysql.format(sql, inserts);
        return executeQuery(sql);
    }
};
