var Utilities = {

}
Utilities.CheckHelp = {
    CheckMail: function (mail) {
        var filter = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
        if (filter.test(mail)) return true;
        else {
            return false;
        }
    },
    CheckTwoPwd: function (pwd1, pwd2) {
        return pwd1 == pwd2;
    }
}