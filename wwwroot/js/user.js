String.prototype.hashCode = function() {
    var hash = 0, i, chr;
    if (this.length === 0) return hash;
    for (i = 0; i < this.length; i++) {
        chr   = this.charCodeAt(i);
        hash  = ((hash << 5) - hash) + chr;
        hash |= 0; // Convert to 32bit integer
    }
    return hash;
};

const app = Vue.createApp({
    data() {
        return {
            user: {
                firstName: "",
                middleName: "",
                lastName: "",
                dateOfBirth: "",
                phoneNumber: "",
                role:"",
                events: []
            },
            entities: [],
            isEditing: false,
            copy: ""
        }
    },
    mounted() {
        axios
            .get("/api/user/get-user-info")
            .then(response => {
                this.user.firstName = response.data.firstName
                this.user.middleName = response.data.middleName
                this.user.lastName = response.data.lastName
                this.user.phoneNumber = response.data.phoneNumber
                this.user.dateOfBirth = response.data.dateOfBirth
                this.user.role = response.data.role
                this.user.events = response.data.events
                this.copy = JSON.stringify(this.$root.user)
                console.log(response.data)
            })
            .catch(error => console.log(error))
        /*this.user.firstName = "Иван"
        this.user.middleName = "Иванович"
        this.user.lastName = "Иванов"
        this.user.phoneNumber = "89-98-00"
        this.user.dateOfBirth = "11.11.1911"
        this.user.role = "admin"
        this.user.events = [] */
        this.entities = {
            sport_objects: {
            name: "Спортивные объекты",
            href: "/table/sport-objects",
            color: `hsl(${Math.random()*360},60%,60%)`,
            count: 0
            },
            events: {
                name: "Занятия",
                href: "",
                color: `hsl(${Math.random()*360},60%,60%)`,
                count: 0
            },
            sections: {
                name: "Секции",
                    href: "",
                    color: `hsl(${Math.random()*360},60%,60%)`,
                    count: 0
            },
            subscriptions: {
                name: "Абонементы",
                    href: "",
                    color: `hsl(${Math.random()*360},60%,60%)`,
                    count: 0
            },
            trainers: {
                name: "Тренера",
                    href: "",
                    color: `hsl(${Math.random()*360},60%,60%)`,
                    count: 0
            },
            clients: {
                name: "Клиенты",
                    href: "",
                    color: `hsl(${Math.random()*360},60%,60%)`,
                    count: 0
            },
            admins: {
                name: "Администраторы",
                    href: "",
                    color: `hsl(${Math.random()*360},60%,60%)`,
                    count: 0
            },
            news: {
                name: "Новости",
                    href: "",
                    color: `hsl(${Math.random()*360},60%,60%)`,
                    count: 0
            }
        }

        /* Подсчет количество записей */
        axios
            .get("/api/sport-object/get-number-of-entities")
            .then(response => this.entities.sport_objects.count = response.data)
            .catch(error => console.log(error))
        axios
            .get("/api/section/get-number-of-entities")
            .then(response => this.entities.sections.count = response.data)
            .catch(error => console.log(error))
        axios
            .get("/api/event/get-number-of-entities")
            .then(response => this.entities.events.count = response.data)
            .catch(error => console.log(error))
        axios
            .get("/api/subscription/get-number-of-entities")
            .then(response => this.entities.subscriptions.count = response.data)
            .catch(error => console.log(error))
    }
});

/*app.component("editable-span", {
    template: `<span class="input" :class="{disabled: disabled}" :contenteditable="!disabled" v-html="value" @input="$emit('input', $event.target.innerHTML)"></span>`,
    props: ['value', 'disabled'],
    watch: {
        value: function (newValue) {
            if (document.activeElement === this.$el) {
                return;
            }
            this.$el.innerHTML = newValue;
        }
    }
})*/

app.component("user-info",{
    methods: {
        save() {
            axios
                .put("/api/user/update-user-data", this.$root.user)
                .then(response => {
                    console.log(response)
                    this.$root.isEditing = false
                    this.$root.copy = JSON.stringify(this.$root.user)
                })
                .catch(error => console.log(error))
        },
        discard() {
            this.$root.user = JSON.parse(this.$root.copy)
            console.log(this.$root.user)
            this.$root.isEditing = false
        }
    },
    template: `
        <div class="user">
            <img src="/img/users/user.svg" class="photo" alt=""/>
            <div class="data">
                <div v-if="this.$root.isEditing" class="full-name">
                  <input v-model="this.$root.user.lastName" placeholder="Фамилия">
                  <input v-model="this.$root.user.firstName" placeholder="Имя">
                  <input v-model="this.$root.user.middleName" placeholder="Отчество">
<!--                  <span v-if="this.$root.user.role === 'admin'" class="admin">админстратор</span>-->
                </div>
                <p v-if="!this.$root.isEditing" class="full-name">{{ this.$root.user.lastName }} {{ this.$root.user.firstName }} {{ this.$root.user.middleName }}</p>
                <p class="birth-date">Дата рождения: {{ (new Date(this.$root.user?.dateOfBirth)).toLocaleDateString("ru", {day:"2-digit",month:"2-digit",year:"numeric"}) }} 
                  <span v-if="this.$root.isEditing" class="datepicker-toggle">
                    <span class="datepicker-toggle-button"></span>
                    <input type="date" class="datepicker-input" v-model="this.$root.user.dateOfBirth">
                  </span>
                </p>
    <!--            <p class="e-mail">dostoevskiy@poet.ru</p>-->
                <p class="phone-number">Номер телефона: <input v-model:value="this.$root.user.phoneNumber" :disabled="!this.$root.isEditing"></p>
            </div>
<!--            <div class="edit">
              <svg xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" aria-hidden="true" role="img" preserveAspectRatio="xMidYMid meet" viewBox="0 0 576 512"><path d="M402.3 344.9l32-32c5-5 13.7-1.5 13.7 5.7V464c0 26.5-21.5 48-48 48H48c-26.5 0-48-21.5-48-48V112c0-26.5 21.5-48 48-48h273.5c7.1 0 10.7 8.6 5.7 13.7l-32 32c-1.5 1.5-3.5 2.3-5.7 2.3H48v352h352V350.5c0-2.1.8-4.1 2.3-5.6zm156.6-201.8L296.3 405.7l-90.4 10c-26.2 2.9-48.5-19.2-45.6-45.6l10-90.4L432.9 17.1c22.9-22.9 59.9-22.9 82.7 0l43.2 43.2c22.9 22.9 22.9 60 .1 82.8zM460.1 174L402 115.9L216.2 301.8l-7.3 65.3l65.3-7.3L460.1 174zm64.8-79.7l-43.2-43.2c-4.1-4.1-10.8-4.1-14.8 0L436 82l58.1 58.1l30.9-30.9c4-4.2 4-10.8-.1-14.9z"/></svg>
            </div>-->
            <div class="controls">
              <div v-if="this.$root.isEditing" class="save" @click="this.save">
                <svg xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" aria-hidden="true" role="img" preserveAspectRatio="xMidYMid meet" viewBox="0 0 1024 1024"><path d="M512 64a448 448 0 1 1 0 896a448 448 0 0 1 0-896zm-55.808 536.384l-99.52-99.584a38.4 38.4 0 1 0-54.336 54.336l126.72 126.72a38.272 38.272 0 0 0 54.336 0l262.4-262.464a38.4 38.4 0 1 0-54.272-54.336L456.192 600.384z"/></svg>
              </div>
              <div v-if="this.$root.isEditing" class="discard" @click="this.discard">
                <svg xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" aria-hidden="true" role="img" preserveAspectRatio="xMidYMid meet" viewBox="0 0 1024 1024"><path d="M512 64a448 448 0 1 1 0 896a448 448 0 0 1 0-896zm0 393.664L407.936 353.6a38.4 38.4 0 1 0-54.336 54.336L457.664 512L353.6 616.064a38.4 38.4 0 1 0 54.336 54.336L512 566.336L616.064 670.4a38.4 38.4 0 1 0 54.336-54.336L566.336 512L670.4 407.936a38.4 38.4 0 1 0-54.336-54.336L512 457.664z"/></svg>
              </div>
              <div v-if="!this.$root.isEditing" class="edit" @click="this.$root.isEditing = !this.$root.isEditing">
                <svg xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" aria-hidden="true" role="img" preserveAspectRatio="xMidYMid meet" viewBox="0 0 24 24"><path d="M19.4 7.34L16.66 4.6A2 2 0 0 0 14 4.53l-9 9a2 2 0 0 0-.57 1.21L4 18.91a1 1 0 0 0 .29.8A1 1 0 0 0 5 20h.09l4.17-.38a2 2 0 0 0 1.21-.57l9-9a1.92 1.92 0 0 0-.07-2.71zM16 10.68L13.32 8l1.95-2L18 8.73z"/></svg>
              </div>
            </div>
            <a href="/api/auth/logout" class="logout">
                <svg xmlns="http://www.w3.org/2000/svg"  viewBox="0 0 85 84" fill="none">
                    <path d="M0 9.33333C0 4.2 4.2 0 9.33333 0H46.6667V9.33333H9.33333V74.6667H46.6667V84H9.33333C4.2 84 0 79.8 0 74.6667V9.33333ZM66.1547 37.3333L54.32 25.4987L60.9187 18.9L84.0187 42L60.9187 65.1L54.32 58.5013L66.1547 46.6667H35.42V37.3333H66.1547Z"/>
                </svg>
            </a>
        </div>
    `
}
)

app.component("nearest-events",{
    computed: {
        user() {
            return this.$root.user
        }
    },
    template: `
        <div class="data-block nearest-events">
        <div class="title">Предстоящие занятия:</div>
        <div class="objects-grid events">
          <template v-if="user.events !== null && user.events.length > 0">
            <a v-for="event in user?.events" href="#" class="object event">
              <p class="name">{{ event?.section?.sectionName }}</p>
              <p class="date">{{ (new Date(event?.date)).toLocaleDateString("ru", {day:"2-digit",month:"2-digit",year:"numeric"}) }}</p>
              <p class="time">{{ event?.time }}</p>
            </a>
          </template>
          <p class="empty" v-else>Нет предстоящих событий</p>
        </div>
        </div>
  `
})

app.component("entities-block",{
    computed: {
        entities() {
            return this.$root.entities
        }
    },
    props: [],
    template: `
        <div class="data-block entities-block">
            <div class="title">Таблицы:</div>
            <div class="objects-grid entities">
                <template v-if="entities !== null && Object.keys(entities).length > 0">
                    <a v-for="entity in entities" :href="entity.href" class="object entity" :style="{background: entity.color}">
                        <p class="name">{{ entity.name }}</p>
                        <p class="count">{{ entity.count }}</p>
                    </a>
                </template>
              <p class="empty" v-else>Нет данных</p>
            </div>
        </div>
  `
})

app.component("user-block", {
    computed: {
        user() {
            return this.$root.user
        }
    },
    template: `
      <template v-if="user">
        <user-info></user-info>
        <entities-block v-if="user.role === 'admin'"></entities-block>
        <nearest-events></nearest-events>
      </template>
    `
})

app.mount("#app")