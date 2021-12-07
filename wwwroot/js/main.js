const app = Vue.createApp({
    data() {
        return {
            site_routing: {
                nav_links: {
                    objects: { name: "Объекты", url: "/", is_selected: false},
                    news: { name: "Новости", url: "/news", is_selected: false},
                    contacts: { name: "Контакты", url: "/contacts", is_selected: false}
                },
                auth_links: {
                    sign_in_url: "/login",
                    sign_up_url: "#"
                }
            }
        }
    },
    mounted() {
        let path = window.location.pathname
        for (let key in this.site_routing.nav_links) {
            let link = this.site_routing.nav_links[key]
            link.is_selected = (path === link.url)
        }
    }
})

app.component("default-header",{
    props: ['routing'],
    template:`
        <div class="header">
            <div class="logo">
                <img src="/img/asu-clr.png" alt="Астраханский Государственный Университет" id="asu-logo">
                <div class="description">
                    Астраханский<br>
                    государственный<br>
                    университет<br>
                </div>
            </div>
            <div class="nav">
                <ul>
                    <li v-for="link in routing.nav_links"><a :class="{ selected: link.is_selected }" :href="link.url">{{ link.name }}</a></li>
                </ul>
            </div>
            <div class="auth">
                <a :href="routing.auth_links.sign_in_url" id="sign-in">войти</a>
                <a :href="routing.auth_links.sign_up_url" id="sign-up">зарегистрироваться</a>
            </div>
        </div>
    `
})

app.mount("#app")