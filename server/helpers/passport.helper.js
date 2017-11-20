import passport from 'passport';
import GooglePassport from 'passport-google-oauth';

const GoogleStrategy = GooglePassport.OAuth2Strategy;

export default (app) => {

    app.passport = passport;

    passport.serializeUser((user, cb) => {
        cb(null, user.id);
    });

    passport.deserializeUser((id, cb) => {
        app.db.mysql.user.select.byId(id).then((data) => {
            cb(null, data);
        }).catch((err) => {
            cb(err, null);
        });
    });

    passport.use(new GoogleStrategy({
        clientID: process.env.AUTH_CLIENT_ID,
        clientSecret: process.env.AUTH_CLIENT_SECRET,
        callbackURL: process.env.AUTH_CALLBACK_URL
    }, (accessToken, refreshToken, profile, done) => {
        app.db.mysql.user.select.byId(profile.id).then((user) => {
            console.log(user);
            if (user) {
                return done(null, user);
            } else {
                app.db.mysql.user.create({
                    email: profile.emails[0].value
                }).then((data) => {
                    app.logger.debug("New user created", data);
                    done(null, data);
                }).catch((err) => {
                    app.logger.warn("Failed to create user", err);
                    done(err);
                });
            }
        }).catch((err) => {
            app.logger.warn("Google login error", err);
            done(err);
        });
    }));

    app.use(passport.initialize());
    app.use(passport.session());
};
