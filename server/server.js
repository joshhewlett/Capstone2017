// Loads development environment variables from Capstone2017/server/.env file
import dotenv from 'dotenv'
if (process.env.NODE_ENV !== 'production') {
  dotenv.load();
}
