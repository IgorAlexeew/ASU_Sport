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
            user: null,
            entities: []
        }
    },
    beforeCreate() {

    },
    mounted() {
        axios
            .get("/api/user/get-user-info")
            .then(response => {
                this.user = response.data
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

app.component("user-info",{
    computed: {
        user() {
            return this.$root.user
        }
    },
    template: `
        <div class="user">
            <img src="/img/users/user.svg" class="photo" alt=""/>
            <div class="data">
                <p class="full-name">{{ user?.lastName }} {{ user?.firstName }} {{ user?.middleName }} <span v-if="user.role === 'admin'" class="admin">админстратор</span></p>
                <p class="birth-date">Дата рождения: {{ (new Date(user?.dateOfBirth)).toLocaleDateString("ru", {day:"2-digit",month:"2-digit",year:"numeric"}) }}</p>
    <!--            <p class="e-mail">dostoevskiy@poet.ru</p>-->
                <p class="phone-number">Номер телефона: {{ user?.phoneNumber }}</p>
            </div>
            <a href="/api/auth/logout" class="logout">
                <svg xmlns="http://www.w3.org/2000/svg" width="85" height="84" viewBox="0 0 85 84" fill="none">
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