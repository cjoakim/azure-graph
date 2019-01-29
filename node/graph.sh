#!/bin/bash

# Execute all of the functions implemented by graph.js
# Chris Joakim, Microsoft, 2019/01/29

node graph.js me
node graph.js my_selected_attrs
node graph.js my_photo
node graph.js users
node graph.js top_n_people 2000
node graph.js job_titles 2000
node graph.js search_users_by_name Joakim

echo 'done'
