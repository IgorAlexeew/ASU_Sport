let password_field = $('input[name="password"]'),
    confirm_password_field = $('input[name="confirm_password"]'),
    login_field = $('input[name="login"]'),
    access_code_field = $('input[name="access_code"]');

/* Авторизация */
$('.login-btn').on("click", function (event) {
    event.preventDefault();

    login_field.removeClass('error');
    password_field.removeClass('error');

    let login = login_field.val();
    let password = password_field.val();

    $.ajax({
        url: '/admin/src/sign_in.php',
        type: 'POST',
        dataType: 'json',
        data: {
            login: login,
            password: password
        },
        success (data) {
            console.log(data);
            if (data.status)
            {
                login_field.removeClass('error').addClass('passed');
                password_field.removeClass('error').addClass('passed');
                document.location.href = '/admin';
            }
            else
            {
                if (data.type === "wrong_password")
                {
                    password_field.addClass('error').removeClass('passed');
                }
                if (data.type === "no_user")
                {
                    login_field.addClass('error').removeClass('passed');
                    password_field.addClass('error').removeClass('passed');
                }
            }
        }
    })
});


/* Регистрация */
// let password_field = $('input[name="password"]'),
//     confirm_password_field = $('input[name="confirm_password"]'),
//     login_field = $('input[name="login"]'),
//     access_code_field = $('input[name="access_code"]');

$('.confirm-btn').on("click", function (event) {
    event.preventDefault();

    let has_error = false;

    access_code_field.removeClass('error');

    if (login_field.val() === '')
    {
        login_field.addClass('error');
        has_error = true;
    }
    if (access_code_field.val() === '')
    {
        access_code_field.addClass('error');
        has_error = true;
    }
    if (password_field.val() === '')
    {
        password_field.addClass('error');
        has_error = true;
    }
    if (confirm_password_field.val() === '')
    {
        confirm_password_field.addClass('error');
        has_error = true;
    }

    if (!has_error)
    {
        $.ajax({
            url: '/admin/src/sign_up.php',
            type: 'POST',
            dataType: 'json',
            data: {
                login: login_field.val(),
                password: password_field.val(),
                access_code: access_code_field.val()
            },
            success (data) {
                console.log(data);
                if (data.status)
                {
                    console.log("We did it!");
                    login_field.removeClass('error').addClass('passed').text();
                    $('p#login_match').addClass('hidden');
                    document.location.href = '/admin';
                }
                else
                {
                    if (data.type === "username_is_already_taken")
                    {
                        login_field.addClass('error').removeClass('passed');
                        $('p#login_match').addClass('error').removeClass('hidden').text(data.message);
                    }
                }
            }
        })
    }

});

function password_check(){
    $('#password_match').addClass('animated');
    if (password_field.val() === confirm_password_field.val() && password_field.val() !== '') {
        confirm_password_field.removeClass('error').addClass('passed')
        password_field.removeClass('error').addClass('passed');
        $('p#password_match').removeClass('error').addClass('passed').addClass('hidden').text('Пароли совпадают');
    }
    else if (confirm_password_field.val() === '')
    {
        confirm_password_field.removeClass('error').removeClass('passed')
        password_field.removeClass('error').removeClass('passed');
        $('p#password_match').addClass('hidden');
    }
    else
    {
        confirm_password_field.removeClass('passed').addClass('error');
        password_field.removeClass('passed').addClass('error');
        $('p#password_match').text((password_field.val() !== '') ? 'Пароли не совпадают' : 'Введите пароль').removeClass('hidden').removeClass('passed').addClass('error');
    }
}

password_field.change(password_check);
confirm_password_field.change(password_check);

function login_check()
{
    // let login_regexp = /^([a-zA-Z0-9]+[-_]?[a-zA-Z0-9]+)+$/;
    let login_regexp = /^[a-zA-Z][a-zA-Z0-9-_.]{1,20}$/;
    let login = login_field.val();

    if (login !== '')
    {
        if (!login_regexp.test(login))
        {
            login_field.addClass('error').removeClass('passed');
            $('p#login_match').addClass('error').removeClass('hidden').text('Неверный формат логина');
        }
        else
        {
            login_field.addClass('passed').removeClass('error');
            $('p#login_match').addClass('hidden');
        }
    }
    else
    {
        login_field.removeClass('passed').removeClass('error');
        $('p#login_match').addClass('hidden');
    }
}

login_field.change(login_check);
access_code_field.change(function () {
    if (access_code_field.val() === '')
        access_code_field.removeClass('passed');
    else
        access_code_field.removeClass('error').addClass('passed');
})