/* Default header */

export const header_component = {
    data() {
        return {
            nav_links: {
                objects: { name: "Главная", url: "/", is_selected: false},
                news: { name: "Новости", url: "/news", is_selected: false},
                contacts: { name: "Контакты", url: "/contacts", is_selected: false}
            },
            auth_links: {
                sign_in_url: "/login",
                sign_up_url: "/registration"
            },
            _user: null
        }
    },
    computed: {
        username() {
            axios
                .get("/api/user/get-user-info")
                .then(response => {
                    if (response.data.type !== "not_authorized")
                        this._user = response.data.lastName + " " + response.data.firstName + " " + response.data.middleName
                })
                .catch(error => console.log(error));
            return this._user
        }
    },
    mounted() {
        let path = window.location.pathname
        for (let key in this.nav_links) {
            let link = this.nav_links[key]
            link.is_selected = (path === link.url)
        }
    },
    template:`
        <div class="header white-background">
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
                    <li v-for="link in this.nav_links"><a :class="{ selected: link.is_selected }" :href="link.url">{{ link.name }}</a></li>
                </ul>
            </div>
            <div v-if="this.username === null" class="auth">
                <a :href="this.auth_links.sign_in_url" id="sign-in">Войти</a>
                <a :href="this.auth_links.sign_up_url" id="sign-up">Зарегистрироваться</a>
            </div>
            <div class="user" v-else>
                <a href="/user" class="username">{{ username }}</a>
                <a href="/api/auth/logout" class="logout">Выйти</a>
            </div>
        </div>
    `
}

export const loader = {
    props: ['background'],
    template: `
    <div class="lds-grid">
        <div :style="{background: this.background}"></div>
        <div :style="{background: this.background}"></div>
        <div :style="{background: this.background}"></div>
        <div :style="{background: this.background}"></div>
        <div :style="{background: this.background}"></div>
        <div :style="{background: this.background}"></div>
        <div :style="{background: this.background}"></div>
        <div :style="{background: this.background}"></div>
        <div :style="{background: this.background}"></div>
    </div>
`
}

export const modal = {
    props: ['show'],
    data() {
        return {
            overlayStyle: {
                position: 'relative',
                background: '#00000094',
                zIndex: 205,
                width: '100%',
                height: '100%',
                display: 'flex',
                alignItems: 'center',
                justifyContent: 'center',
                backdropFilter: 'blur(10px)',
            },
            modalWrapper: {
                width: '400px',
                background: '#fff',
                borderRadius: '20px',
            },
            modalContent: {
                margin: '30px',
            }
        }
    },
    computed: {
        modalStyle() {
            return {
                position: 'fixed',
                top: 0,
                right: 0,
                bottom: 0,
                left: 0,
                width: '100vw',
                height: '100vh',
                zIndex: 20,
                display: this.show ? 'inherit' : 'none'
            }
        }
    },
    template: `
    <div class="modal" :style="modalStyle" @click="$emit('close')">
        <div class="overlay" :style="overlayStyle">
            <div class="modal-wrapper" :style="modalWrapper" @click.stop="">
                <div class="modal-content" :style="modalContent">
                    <slot></slot>
                </div>
            </div>
        </div>
    </div>
`,
}