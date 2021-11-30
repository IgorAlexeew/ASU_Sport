const app = Vue.createApp({

})

app.config.API_ROOT = ""

app.component("login-form",{
    data() {
        return {
            form_data: {
                login: null,
                password: null
            }
        }
    },
    methods: {
        login() {
            axios
                .post(app.config.API_ROOT + "api/auth/sign-in", this.form_data)
                .then(response => {
                    console.log(response)
                })
                .catch(error => { console.log(error) })
        }
    },
    template: `
        <form @submit.prevent="login" action="" class="sign-in">
            <p class="title">Вход</p>
            <input type="text" class="login" placeholder="Логин" v-model="form_data.login">
            <input type="password" class="password" placeholder="Пароль" v-model="form_data.password">
            <button class="login-button" type="submit">войти</button>
        </form>
    `
})
app.mount("#app")