import dbConnection from './helper';

export default async(sql) => {
  let connection = await dbConnection();
  try {
    let results = await connection.query(sql);
    console.log(results);
  } catch (error) {
    throw error;
  }
  return results;
};
