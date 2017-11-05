// Loads development environment variables from Capstone2017/server/.env file
if (process.env.NODE_ENV !== 'production') {
  require('dotenv').load();
}
