import executeQuery from '../query';
import {
    DBConfig
} from "../config";

var mysql = require('mysql');
var table = DBConfig.dbs.presentation;

export default {
    byId: async(id, presentation) => {
        if (!id || !presentation.name || !presentation.description || !presentation.presented || !presentation.user_id) {
            throw "Missing attribute on presentation";
        }
        var sql = "UPDATE " + table + " SET name = ?, description = ?, presented = ?, user_id = ? WHERE id = ?"
        var inserts = [presentation.name, presentation.description, presentation.presented, presentation.user_id, id];
        sql = mysql.format(sql, inserts);
        return executeQuery(sql);
    }
};
