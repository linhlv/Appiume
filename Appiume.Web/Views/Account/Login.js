(function () {

    $(function() {
        $('#LoginButton').click(function (e) {
            e.preventDefault();
            Appiume.Apm.ui.setBusy(
                $('#LoginArea'),
                Appiume.Apm.ajax({
                    url: Appiume.Apm.appPath + 'Account/Login',
                    type: 'POST',
                    data: JSON.stringify({
                        tenancyName: $('#TenancyName').val(),
                        usernameOrEmailAddress: $('#EmailAddressInput').val(),
                        password: $('#PasswordInput').val(),
                        rememberMe: $('#RememberMeInput').is(':checked')
                    })
                })
            );
        });

        $('a.social-login-link').click(function () {
            var $a = $(this);
            var $form = $a.closest('form');
            $form.find('input[name=provider]').val($a.attr('data-provider'));
            $form.submit();
        });

        $('#LoginForm input:first-child').focus();
    });

})();