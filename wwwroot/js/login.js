const app = Vue.createApp({})

const ROOT = "https://asu-sport.azurewebsites.net"
// const API_ROOT = ROOT + "/api"
const API_ROOT = "/api"

app.component("login-form",{
    data() {
        return {
            form_data: {
                login: null,
                password: null
            },
            login_input_class: {
                login: true,
                error: false
            },
            password_input_class: {
                password: true,
                error: false
            }
        }
    },
    methods: {
        login() {
            axios
                .post(API_ROOT + "/sign-in", this.form_data)
                .then(response => {
                    if (response.data.type === "no_user")
                    {
                        this.login_input_class.error = true
                        this.password_input_class.error = true
                    }
                    else if (response.data.type === "wrong_password")
                    {
                        this.password_input_class.error = true
                        this.login_input_class.error = false
                    }
                    else if (response.data.type === "success")
                    {
                        this.login_input_class.error = false
                        this.password_input_class.error = false
                        window.location.href = ROOT + "/user"
                    }
                    console.log(response.data.type === "wrong_password")
                })
                .catch(error => { console.log(error) })
        }
    },
    template: `
        <form @submit.prevent="login" action="" class="sign-in">
            <p class="title">Вход</p>
            <input type="text" :class="login_input_class" placeholder="Логин" v-model="form_data.login" readonly onfocus="this.removeAttribute('readonly')">
            <input type="password" :class="password_input_class" placeholder="Пароль" v-model="form_data.password"  readonly onfocus="this.removeAttribute('readonly')">
            <button class="login-button" type="submit">войти</button>
        </form>
    `
})

app.component("logo-img", {
    props: [],
    template: `
      <div class="logo">
        <a href="/"><img src="/img/asu-clr.png" alt="Астраханский Государственный Университет" id="asu-logo"></a>
      </div>
    `
})

app.component("login-page", {
    props: [],
    template: `
        <div class="content">
            <logo-img></logo-img>
            <login-form></login-form>
        </div>
    `
})
app.mount("#app")