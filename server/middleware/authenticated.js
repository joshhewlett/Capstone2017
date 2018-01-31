/**
 * Ensures the user has been attached to the request by passport
 * Route specific middleware
 */
export default (req, res, next) => {
    if (process.env.FAKE_USER_AUTHENTICATION === "true" || req.user) {
        next();
    } else {
        throw {
            status: 403,
            message: "Authentication Error"
        };
    }
};
