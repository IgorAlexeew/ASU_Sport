/*let password_field = $('input[name="password"]'),
    confirm_password_field = $('input[name="confirm_password"]'),
    login_field = $('input[name="login"]');

/!* Авторизация *!/
$('.login-btn').on("click", function (event) {
    event.preventDefault();

    login_field.removeClass('error');
    password_field.removeClass('error');

    let login = login_field.val();
    let password = password_field.val();

    $.ajax({
        url: '/api/auth/signin',
        type: 'POST',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({
            login: login,
            password: password
        }),
        /!*headers: {
            RequestVerificationToken:
                $('input:hidden[name="__RequestVerificationToken"]').val()
        },*!/
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


$('.confirm-btn').on("click", function (event) {
    event.preventDefault();

    let has_error = false;

    login_field.removeClass('error');
    password_field.removeClass('error');
    confirm_password_field.removeClass('error');
    // access_code_field.removeClass('error');

    if (login_field.val() === '')
    {
        login_field.addClass('error');
        has_error = true;
    }
    /!*if (access_code_field.val() === '')
    {
        access_code_field.addClass('error');
        has_error = true;
    }*!/
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
            url: '/api/auth/sign-up',
            type: 'POST',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({
                login: login_field.val(),
                password: password_field.val()
            }),
            success (data) {
                console.log(data);
                if (data.status)
                {
                    console.log("We did it!");
                    login_field.removeClass('error').addClass('passed').text();
                    $('p#login_match').addClass('hidden');
                    document.location.href = '/user';
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

});*/

import {loader} from "./shared-components.js";

const app = Vue.createApp({})

app.component("sign-up-form", {
    data() {
        return {
            form: {
                login: '',
                password: '',
                lastName: '',
                firstName: '',
                middleName: '',
                dateOfBirth: '',
                phoneNumber: ''
            },
            confirm_password: '',
            password_classes: [],
            login_classes: [],
            last_name_classes: [],
            first_name_classes: [],
            middle_name_classes: [],
            date_of_birth_classes: [],
            phone_number_classes: [],
            login_message_classes: ['hidden'],
            password_message_classes: ['hidden'],
            login_message: "",
            password_message: "",
            isLoading: false
        }
    },
    watch: {
        form: {
            handler(val, oldVal) {
                this.form_check()
            },
            deep: true
        },
        confirm_password(val) {
            this.password_check()
        }
    },
    methods: {
        login_check()
        {
            // let login_regexp = /^([a-zA-Z0-9]+[-_]?[a-zA-Z0-9]+)+$/;
            let login_regexp = /^[a-zA-Z][a-zA-Z0-9-_.]{1,20}$/;
            this.is_login_correct = login_regexp.test(this.form.login);
            if (login_regexp.test(this.form.login)) {
                this.login_classes = ['passed']
                this.login_message = "Все ок"
                this.login_message_classes = ["passed", "hidden"]
            } else if (this.form.login === '') {
                this.login_classes = []
                this.login_message = "Поле логина пустое"
                this.login_message_classes = ["hidden"]
            } else {
                this.login_classes = ['error']
                this.login_message = "Неверный формат"
                this.login_message_classes = ["error"]
            }
        },
        password_check() {
            if (this.form.password === this.confirm_password && this.form.password.length > 3) {
                this.password_classes = ['passed']
                this.password_message = "Все ок"
                this.password_message_classes = ["passed", "hidden"]
            } else if (this.form.password === this.confirm_password && this.form.password === '') {
                this.password_classes = []
                this.password_message = "Поля паролей пустые"
            } else {
                this.password_classes = ['error']
                if (this.form.password !== this.confirm_password) {
                    this.password_message = "Пароли не совпадают"
                    this.password_message_classes = ["error"]
                } else {
                    this.password_message = "Пароль слишком короткий"
                    this.password_message_classes = ["error"]
                }
            }
        },
        form_check() {
            this.login_check()
            this.password_check()
            this.last_name_classes = []
            this.first_name_classes = []
            this.middle_name_classes = []
            this.date_of_birth_classes = []
            this.phone_number_classes = []
        },
        submit() {
            let error = false
            if (this.form.login === '') {
                this.login_classes = ['error']
                error = true
            }
            if (this.form.password === '') {
                this.password_classes = ['error']
                error = true
            }
            if (this.form.lastName === '') {
                this.last_name_classes = ['error']
                error = true
            }
            if (this.form.firstName === '') {
                this.first_name_classes = ['error']
                error = true
            }
            if (this.form.middleName === '') {
                this.middle_name_classes = ['error']
                error = true
            }
            if (this.form.dateOfBirth === '') {
                this.date_of_birth_classes = ['error']
                error = true
            }
            if (this.form.phoneNumber === '') {
                this.phone_number_classes = ['error']
                error = true
                console.log("HI")
            }

            if (!error) {
                this.isLoading = true
                axios
                    .post("/api/auth/sign-up", this.form)
                    .then(response => {
                        if (response.status)
                        {
                            this.form_check()
                            document.location.href = '/user';
                        }
                        else
                        {
                            if (data.type === "username_is_already_taken")
                            {
                                this.login_classes = ['error']
                                this.login_message_classes = ['error']
                                this.login_message = "Логин уже занят"
                            }
                        }
                        this.isLoading = false
                    })
            }
        }
    },
    template: `
    <form>
        <p class="form_title">Регистрация</p>
        <label for="login">Логин:</label>
        <input :class="this.login_classes" type="text" name="login" id="login" v-model="this.form.login" readonly onfocus="this.removeAttribute('readonly')">
        <p class="message" :class="this.login_message_classes" id="login_match">{{this.login_message}}</p>
        
        <label for="password">Пароль:</label>
        <input :class="this.password_classes" type="password" name="password" id="password" v-model="this.form.password" readonly onfocus="this.removeAttribute('readonly')">
        
        <label for="confirm_password">Подтвердите пароль:</label>
        <input :class="this.password_classes" type="password" name="confirm_password" id="confirm_password" v-model="this.confirm_password">
        <p class="message" :class="this.password_message_classes" id="password_match">{{this.password_message}}</p>

        <label for="login">Фамилия:</label>
        <input :class="this.last_name_classes" type="text" name="last_name" id="last_name" v-model="this.form.lastName" readonly onfocus="this.removeAttribute('readonly')">

        <label for="login">Имя:</label>
        <input :class="this.first_name_classes" type="text" name="first_name" id="first_name" v-model="this.form.firstName" readonly onfocus="this.removeAttribute('readonly')">

        <label for="login">Отчество:</label>
        <input :class="this.middle_name_classes" type="text" name="middle_name" id="middle_name" v-model="this.form.middleName" readonly onfocus="this.removeAttribute('readonly')">

        <label for="login">Дата рождения:</label>
        <input :class="this.date_of_birth_classes" type="date" name="date_of_birth" id="date_of_birth" v-model="this.form.dateOfBirth" readonly onfocus="this.removeAttribute('readonly')">

        <label for="login">Номер телефона:</label>
        <input :class="this.phone_number_classes" type="tel" pattern="[+]{1}[0-9]{11,14}" name="phone_number" id="phone_number" v-model="this.form.phoneNumber" readonly onfocus="this.removeAttribute('readonly')" placeholder="+7 (999) 999-99-99">


        <button type="submit" class="confirm-btn" @click.prevent="submit">Подтвердить</button>
    </form>
    <div v-if="this.isLoading" class="loader-wrap">
        <loader></loader>
    </div>
    `
})

app.component("asu-header", {
    props: [],
    template: `
    <header>
        <img id="asu_logo" src="/img/asu-clr.png" alt="Астраханский Государственный Университет">
        <div class="header_text">
            <div class="logo">АГУ<br>СПОРТ</div>
            <div class="title">Быстрее Выше Сильнее</div>
        </div>
    </header>`
})

app.component("registration-page", {
    components: {
        loader: loader
    },
    props: [],
    template: `
    <asu-header></asu-header>
    <sign-up-form></sign-up-form>
    `
})

app.mount("#app")

$(function(){
    //2. Получить элемент, к которому необходимо добавить маску
    $("#phone_number").mask("+7 (999) 999-99-99");
});