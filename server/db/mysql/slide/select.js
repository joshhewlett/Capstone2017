import executeQuery from '../query';
import {
    DBConfig
} from "../../config";

var mysql = require('mysql');
var table = DBConfig.mysql.dbs.slide;

export default {
    byId: async(id) => {
        var sql = "SELECT * FROM " + table + " WHERE id=?";
        sql = mysql.format(sql, [id]);
        return executeQuery(sql);
    },

    byPresentationId: async(id) => {
        var sql = "SELECT " + table + ".id, " + table + ".physician_id, " + table + ".child_id, " + table + ".name, " + table + ".reason, " + table + ".dosage, " + table + ".original_amount, " + table + ".units, " + table + ".date FROM (" + table + " JOIN " + DBConfig.mysql.dbs.child + " ON " + DBConfig.mysql.dbs.child + ".id=" + table + ".child_id) WHERE " + DBConfig.mysql.dbs.child + ".id=?";
        sql = mysql.format(sql, [id]);
        return executeQuery(sql);
    },

    all: async() => {
        var sql = "SELECT * FROM " + table;
        return executeQuery(sql);
    }
};
