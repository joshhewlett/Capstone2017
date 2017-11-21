// Executes Query on SQL database
import dbConnection from './helper';

export default async(sql) => {
    let connection = await dbConnection();
    let results;
    try {
        results = await connection.query(sql);
    } catch (error) {
        throw error;
    }
    return results;
};
