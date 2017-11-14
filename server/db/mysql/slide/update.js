import executeQuery from '../query';
import {
    DBConfig
} from "../../config";

var mysql = require('mysql');
var table = DBConfig.mysql.dbs.slide;

export default {
    byId: async(id, slide) => {
        if (!id || !slide.sequence) {
            throw "Missing attribute on slide";
        }
        var sql = "UPDATE " + table + " SET email = ? WHERE id = ?"
        var inserts = [slide.sequence, id];
        sql = mysql.format(sql, inserts);
        return executeQuery(sql);
    }
};
