import dbConnection from './helper';

export default async(sql) => {
  var connection = await dbConnection();
  try {
    var results = await connection.query(sql);
    console.log(results);
  } catch (error) {
    throw error;
  }
  return results;
};
